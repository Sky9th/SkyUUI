

using UnityEngine;
using UnityEngine.UIElements;

namespace Sky9th.SkyUUI
{
    public class SKyUUIProproperties
    {

        public static GameObject SkyUUIObject { get; set; }
        public static UIDocument Document { get; set; }
        public static SkyUUIController UIController { get; set; }
        public static Cursor Cursor { get; set; }


        public static void init ()
        {
            SkyUUIObject = GameObject.Find("SkyUUIDocument");
            Document = SkyUUIObject.GetComponent<UIDocument>();
            UIController = Document.GetComponent<SkyUUIController>();
            Cursor = Document.GetComponent<Cursor>();
        }

    }
}
