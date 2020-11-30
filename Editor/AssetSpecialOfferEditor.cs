using UnityEditor;
using UnityEngine;
using UnityEditor.AnimatedValues;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(AssetSpecialOffers))]
public class AssetSpecialOfferEditor : Editor
{
    private AssetSpecialOffers _assetSpecialOffer;

    private SerializedProperty _specialOfferType;




    private void OnEnable()
    {
        _assetSpecialOffer = (AssetSpecialOffers)target;
    }


    public override void OnInspectorGUI()
    {
        foreach (OfferData offerData in _assetSpecialOffer.OfferDatas)
        {
            EditorGUILayout.BeginVertical("Box");
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                {
                    _assetSpecialOffer.OfferDatas.Remove(offerData);
                    break;
                }
                
                GUILayout.Space(10);

                EditorGUILayout.LabelField(offerData.ShopProductName.ToString());

                EditorGUILayout.EndHorizontal();


                GUILayout.Space(10);
                offerData.ShopProductName = (ShopProductNames)EditorGUILayout.EnumPopup("Shop Product Name", offerData.ShopProductName);
                offerData.SpecialOfferType = (SpecialOfferType)EditorGUILayout.EnumPopup("Offer Type", offerData.SpecialOfferType);

                GUILayout.Space(20);

                
                offerData.OldPrice = (float)EditorGUILayout.FloatField("Old Price", offerData.OldPrice);
                offerData.NewPrice = (float)EditorGUILayout.FloatField("New Price", offerData.NewPrice);

                if (offerData.SpecialOfferType == SpecialOfferType.Product)
                {
                    offerData.AddingValue = EditorGUILayout.IntField("Adding Value", offerData.AddingValue);
                    GUILayout.Space(20);
                    offerData.Icon = (Sprite)EditorGUILayout.ObjectField("Icon", offerData.Icon, typeof(Object), false);
                }
            }
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Add Special Offer"))
        {
            _assetSpecialOffer.OfferDatas.Add(new OfferData());
        }

        if (GUI.changed)
            SetDirtyScene();
    }


    private void SetDirtyScene()
    {
        EditorUtility.SetDirty(_assetSpecialOffer);
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}
