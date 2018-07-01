function SurveyOption(obj) {
	this.SurveyId = ko.observable(0);
	this.OptionNumber = ko.observable(0);
	this.Title = ko.observable("");
	this.Count = ko.observable(0);

	if (obj != null) {
		this.SurveyId(obj.SurveyId);
		this.OptionNumber(obj.OptionNumber);
		this.Title(obj.Title);
		this.Count(obj.Count);

	}
}