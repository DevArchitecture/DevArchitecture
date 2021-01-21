import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LookUp } from 'app/core/models/lookUp';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from '../../admin/login/services/auth.service';
import { Translate } from './Models/Translate';
import { TranslateService } from './Services/Translate.service';
import { Subject } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import { DataTableDirective } from 'angular-datatables';

declare var jQuery: any;

@Component({
	selector: 'app-translate',
	templateUrl: './translate.component.html',
	styleUrls: ['./translate.component.scss']
})
export class TranslateComponent implements OnDestroy,AfterViewInit, OnInit {
	
	@ViewChild(DataTableDirective) dtElement: DataTableDirective;
	

	translateList:Translate[];
	translate:Translate=new Translate();

	translateAddForm: FormGroup;

	langugelookUp:LookUp[];


	translateId:number;
	dtTrigger:  Subject<any>=new Subject<any>();

	constructor(private translateService:TranslateService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }
	
	ngAfterViewInit(): void {
		this.getTranslateList();
	}

	ngOnDestroy(): void {
		this.dtTrigger.unsubscribe();
	  }

	ngOnInit() {
		this.lookupService.getLanguageLookup().subscribe(data=>{
			this.langugelookUp=data;
		})
		
		this.createTranslateAddForm();
	}


	getTranslateList() {
		this.translateService.getTranslateList().subscribe(data => {
			this.translateList = data;
			this.rerender();
		});
	}

	save(){

		if (this.translateAddForm.valid) {
			this.translate = Object.assign({}, this.translateAddForm.value)

			if (this.translate.id == 0)
				this.addTranslate();
			else
				this.updateTranslate();
	
		}

	}

	addTranslate(){

		this.translateService.addTranslate(this.translate).subscribe(data => {
			this.getTranslateList();
			this.translate = new Translate();
			jQuery('#translate').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.translateAddForm);

		})

	}

	updateTranslate(){

		this.translateService.updateTranslate(this.translate).subscribe(data => {
			debugger;

			var index=this.translateList.findIndex(x=>x.id==this.translate.id);
			this.translateList[index]=this.translate;

			this.rerender();
			this.translate = new Translate();
			jQuery('#translate').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.translateAddForm);

		})

	}

	createTranslateAddForm() {
		this.translateAddForm = this.formBuilder.group({
			id: [0],
			langid:[0, Validators.required],
            code:["", Validators.required],
            value:["", Validators.required]
		})
	}

	deleteTranslate(translateId:number){
		this.translateService.deleteTranslate(translateId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.translateList=this.translateList.filter(x=> x.id!=translateId);
			this.rerender();
		})
	}

	getTranslateById(translateId:number){
		this.clearFormGroup(this.translateAddForm);
		this.translateService.getTranslate(translateId).subscribe(data=>{
			this.translate=data;
			this.translateAddForm.patchValue(data);
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
		debugger;
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
