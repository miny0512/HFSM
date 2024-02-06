﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IState
{
    void Enter();
    void Exit();
    void UpdateState();
    void FixedUpdateState();
}