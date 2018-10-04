using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.GameFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleShip.GameFolder
{
    class Enumerate
    {
    }


    public enum Team
    {
        Player,
        Enemy
    }

    public enum Status
    {
        None,
        Hit,
        Miss
    }

    public enum Phases
    {
        Deploy,
        PlayerTurn,
        EnemyTurn,
        GameOver
    }



}
