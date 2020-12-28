import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertifyService } from 'app/core/services/Alertify.service';
import { LookUpService } from 'app/core/services/LookUp.service';
import { AuthService } from '../../admin/login/Services/Auth.service';
import { Language } from './Models/Language';
import { LanguageService } from './Services/Language.service';

declare var jQuery: any;

@Component({
	selector: 'app-language',
	templateUrl: './language.component.html',
	styleUrls: ['./language.component.scss']
})
export class LanguageComponent implements OnInit {

	

	languageList:Language[];
	language:Language=new Language();

	languageAddForm: FormGroup;


	languageId:number;

	constructor(private languageService:LanguageService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

	ngOnInit() {

		this.getLanguageList();
		this.createLanguageAddForm();
	}


	getLanguageList() {
		this.languageService.getLanguageList().subscribe(data => {
			this.languageList = data
		});
	}

	save(){

		if (this.languageAddForm.valid) {
			this.language = Object.assign({}, this.languageAddForm.value)

			if (this.language.id == 0)
				this.addLanguage();
			else
				this.updateLanguage();
		}

	}

	addLanguage(){

		this.languageService.addLanguage(this.language).subscribe(data => {
			this.getLanguageList();
			this.language = new Language();
			jQuery('#language').modal('hide');
			this.alertifyService.success('Ba?ar?l?');
			this.clearFormGroup(this.languageAddForm);

		})

	}

	updateLanguage(){

		this.languageService.updateLanguage(this.language).subscribe(data => {

			var index=this.languageList.findIndex(x=>x.id==this.language.id);
			this.languageList[index]=this.language;

			this.language = new Language();
			jQuery('#language').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.languageAddForm);

		})

	}

	createLanguageAddForm() {
		this.languageAddForm = this.formBuilder.group({
			id: [0],
			name:["", Validators.required],
			code:["", Validators.required]   
		})
	}

	deleteLanguage(languageId:number){
		this.languageService.deleteLanguage(languageId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.languageList=this.languageList.filter(x=> x.id!=languageId);
		})
	}

	getLanguageById(languageId:number){
		this.clearFormGroup(this.languageAddForm);
		this.languageService.getLanguage(languageId).subscribe(data=>{
			this.language=data;
			this.languageAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

}
