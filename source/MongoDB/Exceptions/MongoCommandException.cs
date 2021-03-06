﻿using System;

namespace MongoDB
{
    /// <summary>
    ///   Raised when a command returns a failure message.
    /// </summary>
    [Serializable]
    public class MongoCommandException : MongoException
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "MongoCommandException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "error">The error.</param>
        /// <param name = "command">The command.</param>
        public MongoCommandException(string message, Document error, Document command)
            : base(message, null)
        {
            Error = error;
            Command = command;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "MongoCommandException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "error">The error.</param>
        /// <param name = "command">The command.</param>
        /// <param name = "e">The e.</param>
        public MongoCommandException(string message, Document error, Document command, Exception e)
            : base(message, e)
        {
            Error = error;
            Command = command;
        }

        /// <summary>
        ///   Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        public Document Error { get; private set; }

        /// <summary>
        ///   Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public Document Command { get; private set; }
    }
}