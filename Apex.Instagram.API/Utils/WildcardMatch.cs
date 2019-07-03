using System.Collections.Generic;

namespace Apex.Instagram.API.Utils
{
    public static class WildcardMatch
    {
        public static bool EqualsWildcard(this string text, string wildcardString)
        {
            var    isLike    = true;
            byte   matchCase = 0;
            char[] filter;
            char[] reversedFilter;
            char[] reversedWord;
            char[] word;
            var    currentPatternStartIndex = 0;
            var    lastCheckedHeadIndex     = 0;
            var    lastCheckedTailIndex     = 0;
            var    reversedWordIndex        = 0;
            var    reversedPatterns         = new List<char[]>();

            if ( text == null || wildcardString == null )
            {
                return false;
            }

            word   = text.ToCharArray();
            filter = wildcardString.ToCharArray();

            //Set which case will be used (0 = no wildcards, 1 = only ?, 2 = only *, 3 = both ? and *
            for ( var i = 0; i < filter.Length; i++ )
            {
                if ( i == '?' )
                {
                    matchCase += 1;

                    break;
                }
            }

            for ( var i = 0; i < filter.Length; i++ )
            {
                if ( filter[i] == '*' )
                {
                    matchCase += 2;

                    break;
                }
            }

            if ( (matchCase == 0 || matchCase == 1) && word.Length != filter.Length )
            {
                return false;
            }

            switch ( matchCase )
            {
                case 0:
                    isLike = text == wildcardString;

                    break;

                case 1:
                    for ( var i = 0; i < text.Length; i++ )
                    {
                        if ( word[i] != filter[i] && filter[i] != '?' )
                        {
                            isLike = false;
                        }
                    }

                    break;

                case 2:
                    //Search for matches until first *
                    for ( var i = 0; i < filter.Length; i++ )
                    {
                        if ( filter[i] != '*' )
                        {
                            if ( filter[i] != word[i] )
                            {
                                return false;
                            }
                        }
                        else
                        {
                            lastCheckedHeadIndex = i;

                            break;
                        }
                    }

                    //Search Tail for matches until first *
                    for ( var i = 0; i < filter.Length; i++ )
                    {
                        if ( filter[filter.Length - 1 - i] != '*' )
                        {
                            if ( filter[filter.Length - 1 - i] != word[word.Length - 1 - i] )
                            {
                                return false;
                            }
                        }
                        else
                        {
                            lastCheckedTailIndex = i;

                            break;
                        }
                    }

                    //Create a reverse word and filter for searching in reverse. The reversed word and filter do not include already checked chars
                    reversedWord   = new char[word.Length - lastCheckedHeadIndex - lastCheckedTailIndex];
                    reversedFilter = new char[filter.Length - lastCheckedHeadIndex - lastCheckedTailIndex];

                    for ( var i = 0; i < reversedWord.Length; i++ )
                    {
                        reversedWord[i] = word[word.Length - (i + 1) - lastCheckedTailIndex];
                    }

                    for ( var i = 0; i < reversedFilter.Length; i++ )
                    {
                        reversedFilter[i] = filter[filter.Length - (i + 1) - lastCheckedTailIndex];
                    }

                    //Cut up the filter into seperate patterns, exclude * as they are not longer needed
                    for ( var i = 0; i < reversedFilter.Length; i++ )
                    {
                        if ( reversedFilter[i] == '*' )
                        {
                            if ( i - currentPatternStartIndex > 0 )
                            {
                                var pattern = new char[i - currentPatternStartIndex];
                                for ( var j = 0; j < pattern.Length; j++ )
                                {
                                    pattern[j] = reversedFilter[currentPatternStartIndex + j];
                                }

                                reversedPatterns.Add(pattern);
                            }

                            currentPatternStartIndex = i + 1;
                        }
                    }

                    //Search for the patterns
                    for ( var i = 0; i < reversedPatterns.Count; i++ )
                    {
                        for ( var j = 0; j < reversedPatterns[i]
                                             .Length; j++ )
                        {
                            if ( reversedWordIndex > reversedWord.Length - 1 )
                            {
                                return false;
                            }

                            if ( reversedPatterns[i][j] != reversedWord[reversedWordIndex + j] )
                            {
                                reversedWordIndex += 1;
                                j                 =  -1;
                            }
                            else
                            {
                                if ( j == reversedPatterns[i]
                                         .Length - 1 )
                                {
                                    reversedWordIndex = reversedWordIndex + reversedPatterns[i]
                                                            .Length;
                                }
                            }
                        }
                    }

                    break;

                case 3:
                    //Same as Case 2 except ? is considered a match
                    //Search Head for matches util first *
                    for ( var i = 0; i < filter.Length; i++ )
                    {
                        if ( filter[i] != '*' )
                        {
                            if ( filter[i] != word[i] && filter[i] != '?' )
                            {
                                return false;
                            }
                        }
                        else
                        {
                            lastCheckedHeadIndex = i;

                            break;
                        }
                    }

                    //Search Tail for matches until first *
                    for ( var i = 0; i < filter.Length; i++ )
                    {
                        if ( filter[filter.Length - 1 - i] != '*' )
                        {
                            if ( filter[filter.Length - 1 - i] != word[word.Length - 1 - i] && filter[filter.Length - 1 - i] != '?' )
                            {
                                return false;
                            }
                        }
                        else
                        {
                            lastCheckedTailIndex = i;

                            break;
                        }
                    }

                    // Reverse and trim word and filter
                    reversedWord   = new char[word.Length - lastCheckedHeadIndex - lastCheckedTailIndex];
                    reversedFilter = new char[filter.Length - lastCheckedHeadIndex - lastCheckedTailIndex];

                    for ( var i = 0; i < reversedWord.Length; i++ )
                    {
                        reversedWord[i] = word[word.Length - (i + 1) - lastCheckedTailIndex];
                    }

                    for ( var i = 0; i < reversedFilter.Length; i++ )
                    {
                        reversedFilter[i] = filter[filter.Length - (i + 1) - lastCheckedTailIndex];
                    }

                    for ( var i = 0; i < reversedFilter.Length; i++ )
                    {
                        if ( reversedFilter[i] == '*' )
                        {
                            if ( i - currentPatternStartIndex > 0 )
                            {
                                var pattern = new char[i - currentPatternStartIndex];
                                for ( var j = 0; j < pattern.Length; j++ )
                                {
                                    pattern[j] = reversedFilter[currentPatternStartIndex + j];
                                }

                                reversedPatterns.Add(pattern);
                            }

                            currentPatternStartIndex = i + 1;
                        }
                    }

                    //Search for the patterns
                    for ( var i = 0; i < reversedPatterns.Count; i++ )
                    {
                        for ( var j = 0; j < reversedPatterns[i]
                                             .Length; j++ )
                        {
                            if ( reversedWordIndex > reversedWord.Length - 1 )
                            {
                                return false;
                            }

                            if ( reversedPatterns[i][j] != '?' && reversedPatterns[i][j] != reversedWord[reversedWordIndex + j] )
                            {
                                reversedWordIndex += 1;
                                j                 =  -1;
                            }
                            else
                            {
                                if ( j == reversedPatterns[i]
                                         .Length - 1 )
                                {
                                    reversedWordIndex = reversedWordIndex + reversedPatterns[i]
                                                            .Length;
                                }
                            }
                        }
                    }

                    break;
            }

            return isLike;
        }
    }
}