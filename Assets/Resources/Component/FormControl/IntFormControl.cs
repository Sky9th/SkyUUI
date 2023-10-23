using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IntFormControl : FormControl<int>
{
    public new class UxmlFactory : UxmlFactory<IntFormControl, UxmlTraits> { }

}
