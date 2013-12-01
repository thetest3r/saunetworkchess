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

        public void RemoveGame()
        {

        }

        public GameLogic.Game GetLastItem()
        {
            lock (syncRoot)
            {
                
            }
            return null;
        }

    }
}
