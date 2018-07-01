function Survey(obj) {
	this.Id = ko.observable(0);
	this.Guid = ko.observable("");
	this.Title = ko.observable("");
	this.CloseDateTime = ko.observable(null);
	this.DeleteDateTime = ko.observable(null);
	this.SurveyOptions = ko.observableArray();

	if (obj != null) {
		this.Id(obj.Id);
		this.Guid(obj.Guid);
		this.Title(obj.Title);
		this.CloseDateTime(obj.CloseDateTime);
		this.DeleteDateTime(obj.DeleteDateTime);
		for (var i = 0; i < obj.SurveyOptions.length; i++) {
			console.log(obj.SurveyOptions[i]);
			this.SurveyOptions.push(new SurveyOption(obj.SurveyOptions[i]));
		}
	}
	//console.log(ko.toJS(this));
}