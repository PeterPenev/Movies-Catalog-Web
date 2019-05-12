using MoviesCatalog.Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCatalog.Services.Providers
{
    public static class BusinessValidator
    {
        public static void IsInProperRange(string input)
        {
            if (input != null && input.Length > 500)
            {
                throw new ArgumentException(ServicesConstants.DescriptionTooLong);
            }
        }

        public static void IsNameInValidRange(string name)
        {
            if (name == null)
            {
                throw new ArgumentException(ServicesConstants.NameCanNotBeNull);
            }

            if ( name.Length > 50 || name.Length < 2)
            {
                throw new ArgumentException(ServicesConstants.NameNotInRange);
            }
        }

        public static void IsTitleInValidRange(string title)
        {
            if (title == null)
            {
                throw new ArgumentException(ServicesConstants.TitleCanNotBeNull);
            }

            if (title.Length < 2 || title.Length > 500)
            {
                throw new ArgumentException(ServicesConstants.TitleTNotInRange);
            }
        }

        public static void IsGenreInValidRange(string title)
        {
            if (title == null)
            {
                throw new ArgumentException(ServicesConstants.GenreCanNotBeNull);
            }

            if (title.Length > 50 || title.Length < 2)
            {
                throw new ArgumentException(ServicesConstants.GenreNotInRange);
            }
        }

        public static void IsTrailerInValidRange(string trailer)
        {
            if (trailer != null && trailer.Length > 500)
            {
                throw new ArgumentException(ServicesConstants.TrailerTooLong);
            }
        }

        public static void IsRatingInRange(double checkRating)
        {
            if (checkRating < 0 || checkRating > 5)
            {
                throw new ArgumentException(ServicesConstants.RatingNotInRange);
            }
        }
    }
}
