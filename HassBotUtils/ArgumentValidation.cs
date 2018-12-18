using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HassBotUtils
{
    public sealed class ArgumentValidation
    {

        private static readonly string ExceptionEmptyString =
            "AV001: The value of '{0}' can not be an empty string.";

        private static readonly string ExceptionInvalidNullNameArgument =
            "AV002: The name for the '{0}' can not be null or an empty string.";

        private static readonly string ExceptionByteArrayValueMustBeGreaterThanZeroBytes =
            "AV003: The value must be greater than 0 bytes.";

        private static readonly string ExceptionExpectedType =
            "AV004: The type is invalid. Expected type '{0}'.";

        private static readonly string ExceptionEnumerationNotDefined =
            "AV005: {0} is not a valid value for {1}.";

        private ArgumentValidation()
        {
        }

        public static void CheckForEmptyString(string variable, string variableName)
        {
            CheckForNullReference(variable, variableName);
            CheckForNullReference(variableName, "variableName");
            if (variable.Length == 0)
            {
                throw new ArgumentException(SafeFormatter.Format(ExceptionEmptyString, variableName));
            }
        }

        public static void CheckForNullReference(object variable, string variableName)
        {
            if (variableName == null)
            {
                throw new ArgumentNullException("variableName");
            }

            if (null == variable)
            {
                throw new ArgumentNullException(variableName);
            }
        }

        public static void CheckForInvalidNullNameReference(string name, string messageName)
        {
            if ((null == name) || (name.Length == 0))
            {
                throw new InvalidOperationException(SafeFormatter.Format(ExceptionInvalidNullNameArgument, messageName));
            }
        }

        public static void CheckForZeroBytes(byte[] bytes, string variableName)
        {
            CheckForNullReference(bytes, "bytes");
            CheckForNullReference(variableName, "variableName");
            if (bytes.Length == 0)
            {
                throw new ArgumentException(ExceptionByteArrayValueMustBeGreaterThanZeroBytes, variableName);
            }
        }

        public static void CheckExpectedType(object variable, Type type)
        {
            CheckForNullReference(variable, "variable");
            CheckForNullReference(type, "type");
            if (!type.IsAssignableFrom(variable.GetType()))
            {
                throw new ArgumentException(SafeFormatter.Format(ExceptionExpectedType, type.FullName));
            }
        }

        public static void CheckEnumeration(Type enumType, object variable, string variableName)
        {
            CheckForNullReference(variable, "variable");
            CheckForNullReference(enumType, "enumType");
            CheckForNullReference(variableName, "variableName");

            if (!Enum.IsDefined(enumType, variable))
            {
                throw new ArgumentException(SafeFormatter.Format(ExceptionEnumerationNotDefined, variable.ToString(), enumType.FullName), variableName);
            }
        }
    }

    public abstract class SafeFormatter
    {

        protected SafeFormatter()
        {
            // .ctor
        }

        public static string Format(string format, params Object[] args)
        {
            ArgumentValidation.CheckForEmptyString(format, "format");
            try
            {
                return string.Format(format, args);
            }
            catch
            {
                return format;
            }
        }

        public static string FormatNotify(string format, params Object[] args)
        {
            ArgumentValidation.CheckForEmptyString(format, "format");
            try
            {
                return string.Format(format, args);
            }
            catch (Exception e)
            {
                return format + " [" + e.Message + "]";
            }
        }
    }
}