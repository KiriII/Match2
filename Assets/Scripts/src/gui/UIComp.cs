using UnityEngine;

namespace Match3Core.gui
{
    public class UIComp : MonoBehaviour
    {
        public void Awake()
        {
            Validate();
        }

        public virtual void Validate()
        {

        }

        public void AssertNotNull(GameObject obj, string objName)
        {
            if (obj == null)
            {
                PrintAssert(objName, this.gameObject);
            }
        }

        public void AssertNotNull(Component obj, string objName)
        {
            if (obj == null)
            {
                PrintAssert(objName, this.gameObject);
            }
        }

        public void AssertNotNull(object obj, string objName)
        {
            if (obj == null)
            {
                PrintAssert(objName, this.gameObject);
            }
        }

        private void PrintAssert(string name, GameObject gameObject)
        {
            Debug.LogAssertion($"Missing component {name} in {this.gameObject.name}");
        }
    }
}