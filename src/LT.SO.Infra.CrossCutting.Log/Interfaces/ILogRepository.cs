﻿using LT.SO.Infra.CrossCutting.Log.Entities;

namespace LT.SO.Infra.CrossCutting.Log.Interfaces
{
    public interface ILogRepository : IRepository<LogModel>
    {
    }
}