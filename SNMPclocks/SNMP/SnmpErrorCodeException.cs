﻿using Lextm.SharpSnmpLib;

namespace SNMPclocks.SNMP
{
    internal class SnmpErrorCodeException : Exception
    {

        public ErrorCode ErrorCode { get; private init; }

        public SnmpErrorCodeException(ErrorCode errorCode)
            : base()
            => ErrorCode = errorCode;

        public SnmpErrorCodeException(ErrorCode errorCode, string message)
            : base(message)
            => ErrorCode = errorCode;

        public SnmpErrorCodeException(ErrorCode errorCode, string message, Exception innerException)
            : base(message, innerException)
            => ErrorCode = errorCode;

    }
}
