using UnityEngine;

namespace Sky9th.UUI
{
    public enum ThemeEnum
    {
        DEFAULT
    }

    public enum TypeEnum
    {
        Default,
        Primary,
        Info,
        Warn,
        Disabled
    }
    public enum ValidationEnum
    {
        MatchRegexp,
        IsEmail,
        IsEmpty,
        Required,
        Trim,
        IsNumber,
        IsFloat,
        IsPositive,
        MinNumber,
        MaxNumber,
        MinFloat,
        MaxFloat,
        IsString,
        MinStringLength,
        MaxStringLength,
        MaxFileSize,
        AllowedExtensions
    }

}