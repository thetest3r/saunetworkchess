using System;
using System.Collections.Generic;

namespace ChessGame.GameManager
{
    public sealed class GameManager
    {
        private static volatile GameManager instance;
        private static object syncRoot = new Object();

        private static List<GameLogic.Game> games;


        private GameManager() 
        {
            games =  new List<GameLogic.Game>();
        }

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GameManager();
                    }
                }
                return instance;
            }
        }

        public void AddGame(GameLogic.Game g)
        {
            lock (syncRoot)
            {
                games.Add(g);
            }
        }

        public GameLogic.Game GetReferenceToGame()
        {
            return null;
        }

        public void RemoveGame(int id)
        {
            lock (syncRoot)
            {
                for (int i = 0; i < games.Count; i++)
                {
                    if (games[i].Id == id)
                    {
                        games.RemoveAt(i);
                        return;
                    }
                }
                
            }
            
        }

        public GameLogic.Game GetGame(int id)
        {
            GameLogic.Game game = null;
            lock (syncRoot)
            {
                for (int i = 0; i < games.Count; i++)
                {
                    if (games[i].Id == id)
                    {
                        game = games[i];
                    }
                }

            }
            return game;

        }

        public GameLogic.Game GetLastItem()
        {
            GameLogic.Game game;
            lock (syncRoot)
            {
                game = games[games.Count - 1];
            }
            return game;
        }

    }
}
