import { Component, OnInit,OnDestroy, AfterViewInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from '../../admin/login/services/auth.service';
import { Language } from './Models/Language';
import { LanguageService } from './Services/Language.service';
import { Subject } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import { DataTableDirective } from 'angular-datatables';

declare var jQuery: any;

@Component({
	selector: 'app-language',
	templateUrl: './language.component.html',
	styleUrls: ['./language.component.scss']
})
export class LanguageComponent implements OnDestroy,AfterViewInit, OnInit {

	@ViewChild(DataTableDirective) dtElement: DataTableDirective;
	
	languageList:Language[];
	language:Language=new Language();

	languageAddForm: FormGroup;

	languageId:number;
	dtTrigger:  Subject<any>=new Subject<any>();

	constructor(private languageService:LanguageService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }
	
	ngAfterViewInit(): void {
		this.getLanguageList();
	}

	ngOnDestroy(): void {
		this.dtTrigger.unsubscribe();
	}

	ngOnInit() {
		this.createLanguageAddForm();
	}


	getLanguageList() {
		this.languageService.getLanguageList().subscribe(data => {
			this.languageList = data;
			this.rerender();
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
			this.rerender();
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
			this.rerender();
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

	rerender(): void {
		
		if (this.dtElement.dtInstance == undefined) {
		  this.dtTrigger.next();
		}
		else {
		  this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
	
			// Destroy the table first
			dtInstance.destroy();
			// Call the dtTrigger to rerender again
			this.dtTrigger.next();
		  });
		}
	  }

}
