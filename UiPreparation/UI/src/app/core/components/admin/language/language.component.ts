import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from '../login/services/auth.service';
import { Language } from './Models/Language';
import { LanguageService } from './Services/Language.service';

declare var jQuery: any;

@Component({
	selector: 'app-language',
	templateUrl: './language.component.html',
	styleUrls: ['./language.component.scss']
})
export class LanguageComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id', 'name', 'code','update','delete'];
	
	languageList:Language[];
	language:Language=new Language();

	languageAddForm: FormGroup;

	languageId:number;


	constructor(private languageService:LanguageService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }
	
	ngAfterViewInit(): void {

		this.getLanguageList();
	}

	ngOnInit() {

		this.createLanguageAddForm();
	}

	getLanguageList() {
		this.languageService.getLanguageList().subscribe(data => {
			this.languageList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
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
			this.alertifyService.success(data);
			this.clearFormGroup(this.languageAddForm);

		})

	}

	updateLanguage(){
		this.languageService.updateLanguage(this.language).subscribe(data => {

			var index=this.languageList.findIndex(x=>x.id==this.language.id);
			this.languageList[index]=this.language;
			this.dataSource = new MatTableDataSource(this.languageList);
			this.configDataTable();
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
			this.dataSource = new MatTableDataSource(this.languageList);
			this.configDataTable();
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

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	  }

	  applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

}
