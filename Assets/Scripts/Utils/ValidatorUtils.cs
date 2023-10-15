using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sky9th.UUI
{
    public static class ValidatorUtils<T>
    {

        public static bool Validate(string input, object value)
        {
            string[] param = input.Split(":");
            if (param.Length < 1) { Debug.LogError("Unsupport Validator:" + input + ", value:" + value); return false; }
            Type validatorClass = typeof(ValidatorUtils<T>);
            MethodInfo method = validatorClass.GetMethod(param[0]);
            switch (param[0])
            {
                case "MinFloat":
                case "MaxFloat":
                    float f = float.Parse(param[1]);
                    return (bool)(method.Invoke(null, new object[] { value, f }));
                case "MatchRegexp":
                case "MinStringLength":
                case "MaxStringLength":
                    return (bool)(method.Invoke(null, new object[] { value, param[1] }));
                case "MaxFileSize":
                case "MinNumber":
                case "MaxNumber":
                    int p = int.Parse(param[1]);
                    return (bool)(method.Invoke(null, new object[] { value, p }));
                case "IsEmail":
                case "IsEmpty":
                case "Required":
                case "IsNumber":
                case "IsFloat":
                case "IsPositive":
                    return (bool)(method.Invoke(null, new object[] { value }));
                default:
                    Debug.LogError("Unsupport Validator");
                    return false;
            }
        }

        public static bool Verify<U>(U control) where U : IVerify<T>
        {
            control.RemoveFromClassList("danger");
            VisualElement errorMsgContainer = UIToolkitUtils.FindChildElement(control as VisualElement, ".errorMsg");
            control.errorMsgList = new();
            if (errorMsgContainer != null)
            {
                UIToolkitUtils.ClearChildrenElements(errorMsgContainer);
            }
            control.isError = new bool[control.validatorCallback.Count];
            for (int i = 0; i < control.validatorCallback.Count; i++)
            {
                control.isError[i] = Validate(control.validatorCallback[i], control.value);
                if (!control.isError[i])
                {
                    control.AddToClassList("danger");
                    if (control.errorMsg.Length > 0 && control.errorMsg[i] != null)
                    {
                        control.errorMsgList.Add(control.errorMsg[i]);
                    }
                }
            }
            return control.errorMsgList.Count > 0;
        }

        public static void CheckValidator<U>(U control) where U : IVerify<T>
        {
            control.errorMsg = control.errorMsgStr.Split(",");
            control.validatorCallback = new();
            if (control.validator != null)
            {
                string[] validators = control.validator.Split(",");
                Type validatorClass = typeof(ValidatorUtils<T>);

                MethodInfo[] methodInfos = validatorClass.GetMethods();
                string[] methods = new string[methodInfos.Length];
                for (int i = 0; i < methodInfos.Length; i++)
                {
                    MethodInfo methodInfo = methodInfos[i];
                    methods[i] = methodInfo.Name;
                }
                for (int p = 0; p < validators.Length; p++)
                {
                    if (validators[p] == "" || validators[p] == null) { continue; }

                    string[] v = validators[p].Split(":");

                    if (!methods.Contains(v[0]))
                    {
                        Debug.Log("Unsupport validator:" + v[0]);
                    }
                    else
                    {
                        control.validatorCallback.Add(validators[p]);
                    }
                }
            }
        }

        // Regular expression matching validation
        public static bool MatchRegexp(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        // Validate email address
        public static bool IsEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }

        // Validate if string is empty
        public static bool IsEmpty(string input)
        {
            return string.IsNullOrEmpty(input);
        }

        // Validate if string is required (non-empty)
        public static bool Required(object input)
        {
            if (input == null)
            {
                return false;
            }

            if (input is string inputString)
            {
                return !string.IsNullOrEmpty(inputString.Trim());
            }
            else if (input is bool inputBool)
            {
                return true;
            }
            else if (input is int inputInt)
            {
                return true;
            }
            else if (input is float inputFloat)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Trim whitespace characters from both ends of the string

        // Validate if input is a number
        public static bool IsNumber(string input)
        {
            return int.TryParse(input, out _);
        }

        // Validate if input is a floating-point number
        public static bool IsFloat(string input)
        {
            return float.TryParse(input, out _);
        }

        // Validate if input is a positive number
        public static bool IsPositive(string input)
        {
            if (float.TryParse(input, out float number))
            {
                return number > 0;
            }
            return false;
        }

        // Validate if input is greater than or equal to a minimum number
        public static bool MinNumber(int input, int min)
        {
            return input >= min;
        }

        // Validate if input is less than or equal to a maximum number
        public static bool MaxNumber(int input, int max)
        {
            return input <= max;
        }

        // Validate if input is greater than or equal to a minimum floating-point number
        public static bool MinFloat(float input, float min)
        {
            return input >= min;
        }

        // Validate if input is less than or equal to a maximum floating-point number
        public static bool MaxFloat(float input, float max)
        {
            return input <= max;
        }

        // Validate minimum length of a string
        public static bool MinStringLength(string input, int min)
        {
            return input.Length >= min;
        }

        // Validate maximum length of a string
        public static bool MaxStringLength(string input, int max)
        {
            return input.Length <= max;
        }

        // Validate if file size is less than the maximum limit (in bytes)
        public static bool MaxFileSize(byte[] fileData, int maxSize)
        {
            return fileData.Length <= maxSize;
        }

    }
}