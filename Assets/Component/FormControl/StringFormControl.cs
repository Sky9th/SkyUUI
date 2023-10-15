using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StringFormControl : FormControl<string>
{
    public new class UxmlFactory : UxmlFactory<StringFormControl, UxmlTraits> { }

}
