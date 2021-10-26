using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LightManager.Reflection
{
    public static class MoreReflection
    {
        public static bool HasCustomAttribute<TAttribute>(this ICustomAttributeProvider customAttributeProvider)
            where TAttribute : Attribute
            => customAttributeProvider.GetCustomAttributes(typeof(TAttribute), true).Any();

        public static bool TryGetCustomAttribute<TAttribute>(this ICustomAttributeProvider customAttributeProvider, out TAttribute? attribute)
            where TAttribute : Attribute
        {
            object? attributeObj = customAttributeProvider.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault();

            bool exists = attributeObj is not null;
            attribute = exists ? (TAttribute?)attributeObj : null;

            return exists;
        }

        public static bool IsGenericVersionOf(this Type type, Type openGeneric)
        {
            if (openGeneric == null || !openGeneric.IsGenericTypeDefinition)
                throw new InvalidOperationException($"Parameter must be an open generic type");

            return (type.IsGenericType && !type.IsGenericTypeDefinition && type.GetGenericTypeDefinition() == openGeneric);
        }

        public static IEnumerable<Type> GetGenericVersionsOfInterface(this Type type, Type openGenericInterface)
        {
            if (openGenericInterface == null || !openGenericInterface.IsInterface || !openGenericInterface.IsGenericTypeDefinition)
                throw new InvalidOperationException($"Parameter must be an open generic interface type");

            return type.GetInterfaces().Where(i => i.IsGenericVersionOf(openGenericInterface));
        }

        public static Type GetGenericVersionOfInterface(this Type type, Type openGenericInterface)
            => type.GetGenericVersionsOfInterface(openGenericInterface).Single();

        public static IEnumerable<TAttribute> GetAllAttributes<TAttribute>(this ICustomAttributeProvider customAttributeProvider)
            where TAttribute : Attribute
            => customAttributeProvider.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>();

        public static bool HasGetter(this PropertyInfo property) => property.GetGetMethod() != null;
        public static bool HasSetter(this PropertyInfo property) => property.GetSetMethod() != null;

        public static bool InheritsFrom(this Type type, Type classType, bool canBeEqualToClassType = false)
        {
            if (type.IsSubclassOf(classType))
                return true;

            if (type.IsGenericTypeDefinition)
            {
                Type current = type;

                if (canBeEqualToClassType && type.IsGenericType && type.GetGenericTypeDefinition() == classType)
                    return true;

                do
                {
                    Type baseType = current.BaseType!;

                    if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == classType)
                        return true;

                    current = baseType;
                }
                while (current.IsGenericType);

                return false;
            }
            else
                return canBeEqualToClassType && (type == classType);
        }

        public static bool InheritsFrom<TClass>(this Type type, bool canBeEqualToClassType = false) => type.IsSubclassOf(typeof(TClass)) || (canBeEqualToClassType && (type == typeof(TClass)));
        public static bool Implements<TInterface>(this Type type) => type.Implements(typeof(TInterface));
        public static bool Implements(this Type type, Type interfaceType) => type.IsClass && interfaceType.IsAssignableFrom(type);

        public static TDelegate CreateDelegate<TDelegate>(this MethodInfo method) where TDelegate : Delegate
            => (TDelegate)method.CreateDelegate(typeof(TDelegate));
        public static TDelegate CreateDelegate<TDelegate>(this MethodInfo method, object instance) where TDelegate : Delegate
            => (TDelegate)method.CreateDelegate(typeof(TDelegate), instance);

        public static bool IsCaptureClass(this Type type) => type.Name.Contains("<>c");

        public static MethodInfo GetGenericMethodDefinition(this Type type, string name, BindingFlags bindingAttr) => type
            .GetMethods(bindingAttr)
            .Where(method => method.Name == name)
            .First(method => method.IsGenericMethodDefinition);

        public static MethodInfo GetMethod(this Type type, string name, BindingFlags bindingAttr, Type[] argTypes)
        {
            MethodInfo[] methods = type.GetMethods(bindingAttr);
            MethodInfo? result = methods
                .Where(method => method.Name == name)
                .Where(method => method.GetParameters().Select(param => param.ParameterType).SequenceEqual(argTypes))
                .FirstOrDefault();

            if (result is null)
                throw new MissingMethodException($"Missing method '{name}' on type '{type.FullName}' with arguments of type '{string.Join(", ", argTypes.Select(t => t.Name))}' and BindingFlags '{bindingAttr}'");

            return result;
        }

        public static string GetGenericName(this Type type)
        {
            if (type.IsGenericType)
            {
                string root = type.Name.RemoveAfter("`");
                string genericArgs = string.Join(", ", type.GetGenericArguments().Select(t => t.GetGenericName()));

                return $"{root}<{genericArgs}>";
            }
            else
                return type.Name;
        }

        public static string GetGenericFullName(this Type type)
        {
            string fullName = type.FullName!;
            if (type.IsGenericType)
            {
                string root = fullName.RemoveAfter("`");
                string genericArgs = string.Join(", ", type.GetGenericArguments().Select(t => t.GetGenericFullName()));

                return $"{root}<{genericArgs}>";
            }
            else
                return fullName;
        }

        public static object? GetStaticPrivateField(Type type, string name) => type
            .GetField(name, BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Static)!
            .GetValue(null);

        public static object? GetStaticPrivateField<T>(string name) => GetStaticPrivateField(typeof(T), name);
    }
}