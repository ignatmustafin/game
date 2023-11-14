using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectCollection
{
    public static ObjectCollection _instance;
    public static int gameId;
    private Dictionary<string, ObjectGrid> _objectGrids = new();

    public static void Initialize(int rows, int columns)
    {
        if (_instance == null)
        {
            _instance = new ObjectCollection(rows, columns);
        }
    }

    public ObjectCollection(int Rows, int Columns)
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                var objectName = $"r{row}c{column}";
                Debug.Log(objectName);
                _objectGrids[objectName] = new ObjectGrid()
                {
                    Row = row,
                    Column = column
                };
            }
        }
    }

    public ObjectGrid GetParamsForGameObject(string name)
    {
        return _objectGrids[name];
    }

    public class ObjectGrid
    {
        public int Row;
        public int Column;
    }

    public static async Task<bool> StartGame(int Rows, int Columns)
    {
        HttpService _http = new();
        var qwe = await _http.Post("/api/game/start", new StartGameRequest(2, Rows, Columns));
        StartGameResponse response = JsonUtility.FromJson<StartGameResponse>(qwe);
        gameId = response.gameId;
        return true;
    }
}

class StartGameResponse
{
    public int gameId;
}