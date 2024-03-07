// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0022:Use block body for method", Justification = "I prefer the new style")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Would make the code unclear in this case", Scope = "member", Target = "~M:HamRadioStudy.Models.TestType.GetQuestions~System.Threading.Tasks.Task{System.Collections.Generic.IEnumerable{HamRadioStudy.Models.Question}}")]
[assembly: SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Would make the code unclear in this case", Scope = "member", Target = "~M:HamRadioStudy.Services.QuestionService.GetQuestionsFromWorstSection(System.Int32)~System.Threading.Tasks.Task{System.Collections.Generic.IEnumerable{HamRadioStudy.Models.Question}}")]
[assembly: SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "This code is only called on startup", Scope = "member", Target = "~M:HamRadioStudy.Services.QuestionService.#ctor(HamRadioStudy.IStudyDatabase,System.Boolean)")]
[assembly: SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "<Pending>", Scope = "member", Target = "~M:HamRadioStudy.Utilities.ResourceExtensions.GetResourceLines(System.String)~System.Collections.Generic.IEnumerable{System.String}")]
