﻿using System;
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
        public const string RatingNotInRange = "Rating must be a integer betwen 1 and 5 inclusive!";
        public const string DescriptionTooLong = "Description must be smaller than 500 symbols!";
        public const string TrailerTooLong = "Trailer must smaller than 500 symbols!";
        public const string NameNotInRange = "Name must be grater than 1 and smaller than 50 symbols!";
        public const string TitleTNotInRange = "Title must be greater than 1 and smaller than 100 symbols!";
        public const string GenreNotInRange = "Genre must be grater than 1 and smaller than 50 symbols!";
        public const string NameCanNotBeNull = "Name can not be null";
        public const string TitleCanNotBeNull = "Title can not be null";
        public const string GenreCanNotBeNull = "Genre can not be null";
        public const string NotValidDateFormat = "Movie cannot be created with not valid date {0}!";
        public const string DateNotInRange = "Release date must be betwen {0} and {1}!";
    }
}
