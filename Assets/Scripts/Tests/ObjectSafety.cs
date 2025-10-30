using System;
using System.Collections;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

public class ObjectSafety : MonoBehaviour
{
    [UnityTest]
    public IEnumerator MainMenuScriptReferencesSet()
    {
        yield return TestUtility.LoadSelectedGameScene(0);
        FindScriptReferences();
        yield return null;
    }

    [UnityTest]
    public IEnumerator EditorScriptReferencesSet()
    {
        yield return TestUtility.LoadSelectedGameScene(1);
        FindScriptReferences();
        yield return null;
    }

    private void FindScriptReferences()
    {
        Raymanimator[] scripts = FindObjectsByType<Raymanimator>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        for (int i = 0; i < scripts.Length; i++)
        {
            foreach (var property in scripts[i].GetType().GetFields())
            {
                if (property.GetCustomAttributes(typeof(HideInInspector), true).Length != 0) { continue; }
                if (property.GetCustomAttributes(typeof(NonSerializedAttribute), true).Length != 0) { continue; }

                Assert.NotNull(property.GetValue(scripts[i]), "Field " + property.Name + " of " + scripts[i].name + " is not set.");

                IList list = property.GetValue(scripts[i]) as IList;
                Array array = property.GetValue(scripts[i]) as Array;

                if (list != null)
                {
                    Assert.Greater(list.Count, 0, "List " + property.Name + " in " + scripts[i].name + " is empty.");

                    for (int j = 0; j < list.Count; j++)
                    {
                        Assert.NotNull(list[j], "List item " + j + " of " + property.Name + " in " + scripts[i].name + " is empty.");

                    }
                }

                if (array != null)
                {
                    Assert.Greater(array.Length, 0, "Array " + property.Name + " in " + scripts[i].name + " is empty.");

                    foreach (var item in array)
                    {
                        Assert.NotNull(item, "Array item of " + property.Name + " in " + scripts[i].name + " is empty.");
                    }
                }
            }
        }
    }
}
