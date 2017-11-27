﻿namespace LendYourHome.Common.Constants
{
    public class DataConstants
    {
        public const int EvaluationMinValue = 0;
        public const byte EvaluationMaxValue = 5;

        public const int ReviewTitleMinLength = 5;
        public const int ReviewTitleMaxLength = 30;

        public const int HomeCountryMinLength = 4;
        public const int HomeCountryMaxLength = 48;
        
        public const int HomeCityMinLength = 2;
        public const int HomeCityMaxLength = 30;

        public const int AddressMinLength = 5;
        public const int AddressMaxLength = 40;

        public const string DefaultProfilePictureUrl =
            @"~/Pictures/ProfilePictures/default.jpg";
    }
}
