using System;
using System.Collections.Generic;
using System.Text;

namespace Mapper.App.Commands
{
    internal interface ICommand
    {
        string Execute(params string[] args);
    }
}
