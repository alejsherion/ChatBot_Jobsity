using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppChat.Models
{
    public class ResultBot<T>
    {
        #region Builders
        protected ResultBot() { }

        protected ResultBot(bool isSuccessful, bool isError, T result, string message)
        {
            IsSuccessful = isSuccessful;
            IsError = isError;
            Result = result;
            Message = message;
        }
        #endregion

        #region Members
        public bool IsSuccessful { get; set; }
        public bool IsError { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Set success result for request
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ResultBot<T> SetSuccess(T result) => new ResultBot<T>(true, false, result, string.Empty);
        /// <summary>
        /// Set success result for request
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ResultBot<T> SetUnsuccess() => new ResultBot<T>(false, false, default, string.Empty);
        /// <summary>
        /// Set error result for request
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ResultBot<T> SetError(string message) => new ResultBot<T>(false, true, default, message);
        #endregion
    }
}
