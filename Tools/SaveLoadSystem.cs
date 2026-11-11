using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Units;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

internal static class SaveLoadSystem
{
   
    private static readonly string SaveFilePath = "gamesave.json";

    
    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
      
    };

    public static void SaveGame(List<Army> armysToSave, ITable table)
    {
        GameSnippet gameSnippet = new GameSnippet(armysToSave, table.GetFrames());

        try
        {
          
            string jsonString = JsonSerializer.Serialize(gameSnippet, JsonOptions);

        
            File.WriteAllText(SaveFilePath, jsonString);

            Console.WriteLine($"Игра успешно сохранена в: {Path.GetFullPath(SaveFilePath)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении игры: {ex.Message}");
        }
    }

    public static GameSnippet LoadGame()
    {
        try
        {
            
            if (!File.Exists(SaveFilePath))
            {
             
                return null; 
            }

          
            string jsonString = File.ReadAllText(SaveFilePath);

          
            GameSnippet gameSnippet = JsonSerializer.Deserialize<GameSnippet>(jsonString, JsonOptions);

            Console.WriteLine("Игра успешно загружена!");
           
            return gameSnippet;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке игры: {ex.Message}");
            throw;
        }
    }
}