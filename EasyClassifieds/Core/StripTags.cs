using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace EasyClassifieds.Core
{
    public class CleanHtml
    {
        private static string ReplaceFirst(string haystack, string needle, string replacement)
        {
            int pos = haystack.IndexOf(needle);
            if (pos < 0) return haystack;
            return haystack.Substring(0, pos) + replacement + haystack.Substring(pos + needle.Length);
        }

        private static string ReplaceAll(string haystack, string needle, string replacement)
        {
            int pos;
            // Avoid a possible infinite loop
            if (needle == replacement) return haystack;
            while ((pos = haystack.IndexOf(needle)) > 0)
                haystack = haystack.Substring(0, pos) + replacement + haystack.Substring(pos + needle.Length);
            return haystack;
        }

        public static string StripTags(string Input, string[] AllowedTags)
        {
            Regex StripHTMLExp = new Regex(@"(<\/?[^>]+>)");
            string Output = Input;

            foreach (Match Tag in StripHTMLExp.Matches(Input))
            {
                string HTMLTag = Tag.Value.ToLower();
                bool IsAllowed = false;

                foreach (string AllowedTag in AllowedTags)
                {
                    int offset = -1;

                    // Determine if it is an allowed tag
                    // "<tag>" , "<tag " and "</tag"
                    if (offset != 0) offset = HTMLTag.IndexOf('<' + AllowedTag + '>');
                    if (offset != 0) offset = HTMLTag.IndexOf('<' + AllowedTag + ' ');
                    if (offset != 0) offset = HTMLTag.IndexOf("</" + AllowedTag);

                    // If it matched any of the above the tag is allowed
                    if (offset == 0)
                    {
                        IsAllowed = true;
                        break;
                    }
                }

                // Remove tags that are not allowed
                if (!IsAllowed) Output = ReplaceFirst(Output, Tag.Value, "");
            }

            return Output;
        }

        public static string StripTagsAndAttributes(string Input, string[] AllowedTags)
        {
            /* Remove all unwanted tags first */
            string Output = StripTags(Input, AllowedTags);

            /* Lambda functions */
            MatchEvaluator HrefMatch = m => m.Groups[1].Value + "href..;,;.." + m.Groups[2].Value;
            MatchEvaluator ClassMatch = m => m.Groups[1].Value + "class..;,;.." + m.Groups[2].Value;
            MatchEvaluator UnsafeMatch = m => m.Groups[1].Value + m.Groups[4].Value;

            /* Allow the "href" attribute */
            Output = new Regex("(<a.*)href=(.*>)").Replace(Output, HrefMatch);

            /* Allow the "class" attribute */
            Output = new Regex("(<a.*)class=(.*>)").Replace(Output, ClassMatch);

            /* Remove unsafe attributes in any of the remaining tags */
            Output = new Regex(@"(<.*) .*=(\'|\""|\w)[\w|.|(|)]*(\'|\""|\w)(.*>)").Replace(Output, UnsafeMatch);

            /* Return the allowed tags to their proper form */
            Output = ReplaceAll(Output, "..;,;..", "=");

            return Output;
        }
    }
}