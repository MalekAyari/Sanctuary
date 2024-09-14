using UnityEngine;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using System.IO;
using System;

public class TileCreator : EditorWindow
{
    private Texture2D tileset;
    private int pixelsPerUnit = 32; // Default pixels per unit for the sprites
    private int tileHeight = 16; // Default tile height
    private int tileWidth = 16; // Default tile width
    private int padding = 0; // Optional padding between tiles
    private Tile.TileMaterial material;
    private string folderPath = "...";

    [MenuItem("Tools/Tileset Slicer")]
    public static void ShowWindow()
    {
        GetWindow<TileCreator>("Tileset Slicer");
    }

    private void OnGUI()
    {
        // Select the tileset (PNG image)
        GUILayout.Label("Slice Tileset", EditorStyles.boldLabel);
        tileset = (Texture2D)EditorGUILayout.ObjectField("Tileset Texture", tileset, typeof(Texture2D), false);
        
        // Input for tile width, height, and padding
        GUILayout.Label("Tile Properties", EditorStyles.boldLabel);
        pixelsPerUnit = EditorGUILayout.IntField("Pixels Per Unit: ", pixelsPerUnit);
        tileWidth = EditorGUILayout.IntField("Tile Width: ", tileWidth);
        tileHeight = EditorGUILayout.IntField("Tile Height: ", tileHeight);
        padding = EditorGUILayout.IntField("Padding: ", padding);
        material = (Tile.TileMaterial)EditorGUILayout.EnumPopup("Tileset material:", material);
        
        // Object Path
        GUILayout.Label("Save Directory", EditorStyles.boldLabel);
        folderPath = EditorGUILayout.TextField("Save Path:", folderPath);

        if (GUILayout.Button("Browse...")){
            string path = EditorUtility.OpenFolderPanel("Select directory...", "", "");
            folderPath = path;
        }

        if (GUILayout.Button("Slice Tileset"))
        {
            if (tileset != null)
            {
                SliceTileset();
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please assign a tileset texture!", "OK");
            }
        }
    }

    private void SliceTileset()
    {
        string path = AssetDatabase.GetAssetPath(tileset);
        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);

        if (importer == null)
        {
            EditorUtility.DisplayDialog("Error", "Failed to load tileset...", "OK");
            return;
        }

        // Initialize settings
        importer.isReadable = true;
        importer.spriteImportMode = SpriteImportMode.Multiple;
        importer.filterMode = FilterMode.Point;

        // Edit settings
        var importerSettings = new TextureImporterSettings();
        importer.ReadTextureSettings(importerSettings);
        importerSettings.spriteGenerateFallbackPhysicsShape = false;
        importerSettings.spritePixelsPerUnit = pixelsPerUnit;
        importer.SetTextureSettings(importerSettings);
        
        // Reimport to apply the changes before slicing
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

        // Initialize factory
        var factory = new SpriteDataProviderFactories();
        factory.Init();

        // Initialize dataProvider
        var dataProvider = factory.GetSpriteEditorDataProviderFromObject(importer);
        dataProvider.InitSpriteEditorDataProvider();
        
        // Prepare sprite rects
        int tilesPerRow = tileset.width / (tileWidth + padding);
        int tilesPerColumn = tileset.height / (tileHeight + padding);
        SpriteRect[] spriteRects = new SpriteRect[tilesPerRow * tilesPerColumn];
        
        for (int y = 0; y < tilesPerColumn; y++)
        {
            for (int x = 0; x < tilesPerRow; x++)
            {
                int index = y * tilesPerRow + x;

                spriteRects[index] = new SpriteRect
                {
                    rect = new Rect(x * (tileWidth + padding), tileset.height - (y + 1) * (tileHeight + padding), tileWidth, tileHeight),
                    name = Rename(x,y),
                    pivot = new Vector2(0.5f, 0.5f),
                };
            }
        }

        int j = 0;
        SpriteRect[] newSpriteRects = new SpriteRect[spriteRects.Length -3];
        for (int i = 0; i < spriteRects.Length; i++){
            if (string.IsNullOrEmpty(spriteRects[i].name)) {
                continue;
            }
            newSpriteRects[j] = spriteRects[i];
            j++;
        }
        dataProvider.SetSpriteRects(newSpriteRects);
        dataProvider.Apply();
        
        GenerateTiles(newSpriteRects);
        
        // Reimport to ensure changes are applied
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        AssetDatabase.Refresh();


        EditorUtility.DisplayDialog("Success!", "Successfully sliced sprite!", "OK");
    }

    public string Rename(int x, int y){
        string result = "";
        
        switch (x){

            case 0:
                
                switch (y){

                    case 0:
                        result = "NW_Corner";
                        break;
                    case 1:
                        result = "W_Line";
                        break;
                    case 2:
                        result = "SW_Corner";
                        break;
                    case 3:
                        result = "E_Peak";
                        break;
                    case 4:
                        result = "N_Peak";
                        break;
                    case 5:
                        result = "Diamond";
                        break;
                    case 6:
                        result = "SE_Diagonal";
                        break;
                    case 7:
                        result = "NE_Diagonal";
                        break;
                }
                break;
            case 1:
                
                switch (y){

                    case 0:
                        result = "N_Line";
                        break;
                    case 1:
                        result = "BaseTile";
                        break;
                    case 2:
                        result = "S_Line";
                        break;
                    case 3:
                        result = "W_Peak";
                        break;
                    case 4:
                        result = "S_Peak";
                        break;
                    case 5:
                        result = "R_Diamond";
                        break;
                    case 6:
                        result = "SW_Diagonal";
                        break;
                    case 7:
                        result = "NW_Diagonal";
                        break;
                }
                break;
            case 2:
                
                switch (y){

                    case 0:
                        result = "NE_Corner";
                        break;
                    case 1:
                        result = "E_Line";
                        break;
                    case 2:
                        result = "SE_Corner";
                        break;
                    case 3:
                        result = "Vertical";
                        break; 
                    case 4:
                        result = "Horizontal";
                        break;
                    case 5:
                        break;
                    case 6:
                        result = "N_D_Corners";
                        break;
                    case 7:
                        result = "E_D_Corners";
                        break;
                }
                break;
            case 3:
                
                switch (y){

                    case 0:
                        result = "NW_R_Corner";
                        break;
                    case 1:
                        result = "SW_R_Corner";
                        break;
                    case 2:
                        result = "NW_R_ExtraCorner";
                        break;
                    case 3:
                        result = "SW_R_ExtraCorner";
                        break;
                    case 4:
                        result = "SE_Peak";
                        break;
                    case 5:
                        result = "NE_Peak";
                        break;
                    case 6:
                        result = "W_D_Corners";
                        break;
                    case 7:
                        result = "S_D_Corners";
                        break;
                }
                break;
            case 4:
                
                switch (y){

                    case 0:
                        result = "NE_R_Corner";
                        break;
                    case 1:
                        result = "SE_R_Corner";
                        break;
                    case 2:
                        result = "NE_R_ExtraCorner";
                        break;
                    case 3:
                        result = "SE_R_ExtraCorner";
                        break;
                    case 4:
                        result = "SW_Peak";
                        break;
                    case 5:
                        result = "NW_Peak";
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                }
                break;

        }
        
        return result;
    }

    public void GenerateTiles(SpriteRect[] spriteRects)
{
    foreach (var rect in spriteRects)
    {
        string path = Path.Combine(folderPath, $"{rect.name}.asset");

        if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
        {
            EditorUtility.DisplayDialog("Error", "Invalid folder path!", "OK");
            return;
        }

        path = "Assets" + path.Substring(Application.dataPath.Length).Replace('\\', '/');

        if (AssetDatabase.LoadAssetAtPath<Tile>(path) != null)
        {
            AssetDatabase.DeleteAsset(path); 
        }

        RulePopulator populator = new RulePopulator();
        Tile tile = CreateInstance<Tile>();

        tile.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GetAssetPath(tileset));
        
        if (Enum.TryParse<TileType>(rect.name, out TileType type)){
            tile.type = type;
        } else {
            Debug.LogError("Tile type not found: " + rect.name);
        }

        populator.SetValidNeighbors(tile);

        AssetDatabase.CreateAsset(tile, path);
        AssetDatabase.SaveAssets();
    }
}
}
