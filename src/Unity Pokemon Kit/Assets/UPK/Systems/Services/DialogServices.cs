using UnityEngine;
using System;
using System.Text.RegularExpressions;

/**
 * @Authors : FinalArt (08/2015)
 */
public class DialogServices {

	public static string[] convertToLines(String text, int lineSize) {
		string[] words = text.Split(' ');
		string formattedText = "";
		int characterIndex = 0;
		foreach (string word in words) {
			if (word.Contains("<br>")) {
				formattedText += "\n";
				characterIndex = 0;
			} else {
				string wordWithoutMarkups = Regex.Replace(word, "<.*?>", string.Empty);
				int length = wordWithoutMarkups.Length;
				formattedText += ((characterIndex + length > lineSize) ? "\n" : "") + word + " ";
				characterIndex = ((characterIndex + length > lineSize) ? 0 : characterIndex) + length + 1;
			}
		}
		string[] lines = formattedText.Split ("\n".ToCharArray());
		return lines;
	}

	public static string getWrappedLines(string[] textArray, int linesAmount, int lineIndex) {
		string wrappedLines = "";
		int startIndex = lineIndex;
		int endIndex = (lineIndex + linesAmount <= textArray.Length) ? lineIndex + linesAmount : textArray.Length;
		for (int i = startIndex; i < endIndex; i++) {
			wrappedLines += ((i > startIndex) ? "\n" : "") + textArray[i];
		}
		int offset = (endIndex - startIndex != linesAmount) ? endIndex - startIndex : 0;
		for (int j = 0; j < offset; j++) {
			wrappedLines += "\n";
		}
		return wrappedLines;
	}

}