using System;
using CHROMAVOID.Enemies;
using CHROMAVOID.Tiles;

namespace CHROMAVOID.Core
{
    public static class GameEvents
    {
        public static event Action<Tile, TileState, TileState> TileStateChanged;
        public static event Action<EnemyContainer> EnemySpawned;
        public static event Action<EnemyContainer> EnemyKilled;
        public static event Action<EnemyContainer> EnemyExpired;
        public static event Action<int> WaveStarted;
        public static event Action<int> WaveCompleted;
        public static event Action<int> ScoreChanged;
        public static event Action<Tile> PlayerEnteredBlackTile;
        public static event Action<Tile> PlayerExitedBlackTile;
        public static event Action GameOver;

        public static void RaiseTileStateChanged(Tile tile, TileState previousState, TileState newState)
        {
            TileStateChanged?.Invoke(tile, previousState, newState);
        }

        public static void RaiseEnemySpawned(EnemyContainer enemy)
        {
            EnemySpawned?.Invoke(enemy);
        }

        public static void RaiseEnemyKilled(EnemyContainer enemy)
        {
            EnemyKilled?.Invoke(enemy);
        }

        public static void RaiseEnemyExpired(EnemyContainer enemy)
        {
            EnemyExpired?.Invoke(enemy);
        }

        public static void RaiseWaveStarted(int waveIndex)
        {
            WaveStarted?.Invoke(waveIndex);
        }

        public static void RaiseWaveCompleted(int waveIndex)
        {
            WaveCompleted?.Invoke(waveIndex);
        }

        public static void RaiseScoreChanged(int score)
        {
            ScoreChanged?.Invoke(score);
        }

        public static void RaisePlayerEnteredBlackTile(Tile tile)
        {
            PlayerEnteredBlackTile?.Invoke(tile);
        }

        public static void RaisePlayerExitedBlackTile(Tile tile)
        {
            PlayerExitedBlackTile?.Invoke(tile);
        }

        public static void RaiseGameOver()
        {
            GameOver?.Invoke();
        }
    }
}
