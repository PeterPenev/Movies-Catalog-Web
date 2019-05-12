using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCatalog.Services.Utils
{
    public static class ServicesConstants
    {
        public const string UserAlreadyVoted = "User \"{0}\" has already voted to movie {1}!";
        public const string ReviewNotPresent = "Review not present in database!";
        public const string ReviewNotFromUser = "User \"{0}\" can not edit this review!";
        public const string ActorIsInMovie = "Actor \"{0} {1}\" is already added to movie with title \"{2}\"!";
        public const string GenreIsInMovie = "Genre \"{0}\" is already added to movie with title \"{1}\"!";
    }
}
