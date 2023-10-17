using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoolFormControl : FormControl<int>
{
    public new class UxmlFactory : UxmlFactory<BoolFormControl, UxmlTraits> { }

}
