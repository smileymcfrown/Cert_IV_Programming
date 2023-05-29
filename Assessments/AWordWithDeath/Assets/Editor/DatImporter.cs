using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;
using System.IO;
     
[ScriptedImporter( 2, "ddt" )]
public class DatImporter : ScriptedImporter 
{
    public override void OnImportAsset( AssetImportContext ctx ) {
        Debug.Log("It did something");
        TextAsset subAsset = new TextAsset( File.ReadAllText(ctx.assetPath) );
        ctx.AddObjectToAsset( "text", subAsset );
        ctx.SetMainObject( subAsset );
    }
}

