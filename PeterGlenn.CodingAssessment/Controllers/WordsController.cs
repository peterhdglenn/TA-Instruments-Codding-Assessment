using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PeterGlenn.CodingAssessment.Application;
using PeterGlenn.CodingAssessment.Repositories;

namespace PeterGlenn.CodingAssessment.Controllers
{
    [Route("api/[controller]")]
    public class WordsController : Controller
    {
        private IWordsApplication _wordsApplication;
        public WordsController(IWordsApplication application)
        {
            _wordsApplication = (application != null) ? application : throw new ArgumentNullException("Unable to instantiate WordsApplication becuase the provided WordsApplication is null");
        }

        /// <summary>
        /// The job of the controller is to: 
        ///     - accept the request
        ///     - return the results of the Applciaton layer
        ///     - log any exceptions 
        ///     - protect the user from receiving any nasty System exceptions.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        [HttpGet("[action]/{word}")]
        public List<string> MatchingWords(string word)
        {
            //don't waste time if the input word is null
            if (string.IsNullOrEmpty(word))
                return new List<string>();

            var results = new List<string>();
            try
            {
                //send the requst thru to the Application layer
                results = _wordsApplication.GetMatchingWords(word);
            }
            catch (Exception ex)
            {
                //here is where I would log the exception and translate the exception into a warm and fuzzy messqage for the user.
                //for the sake of this excercise I am just going to re-throw the system exception.
                throw new ApplicationException("Controller - " + ex.Message);
            }
            return results;
        }
    }
}