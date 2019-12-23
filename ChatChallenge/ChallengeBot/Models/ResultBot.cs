using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeBot.Models
{
    public class ResultBot<T>
    {
        #region Builders
        protected ResultBot() { }

        protected ResultBot(bool isSuccessful, T result, string message)
        {
            IsSuccessful = isSuccessful;
            Result = result;
            Message = message;
        } 
        #endregion

        #region Members
        public bool IsSuccessful { get; set; }
        public T Result { get; set; }
        public string Message { get; set; } 
        #endregion

        #region Methods
        /// <summary>
        /// Set success result for request
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ResultBot<T> SetSuccess(T result) => new ResultBot<T>(true, result, string.Empty);
        /// <summary>
        /// Set error result for request
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ResultBot<T> SetError(string message) => new ResultBot<T>(false, default(T), message); 
        #endregion
    }
}
