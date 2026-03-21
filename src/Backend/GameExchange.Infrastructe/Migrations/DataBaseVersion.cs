using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Infrastructe.Migrations
{
    public abstract class DataBaseVersion
    {
        public const int TABLE_USER = 1;
        public const int TABLE_REFRESH_TOKEN = 2;
        public const int TABLE_PLATAFORM_GAME_CATEGORY = 3;
        public const int TABLE_LISTING = 4;
    }
}
