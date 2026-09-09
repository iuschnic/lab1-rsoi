using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions;

public class AppNotFoundException : AppException
{
    public AppNotFoundException(string message) : base(message) { }
    public AppNotFoundException() : base("App-level not found error") { }
}
