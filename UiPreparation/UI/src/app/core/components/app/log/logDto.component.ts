import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertifyService } from 'app/core/services/Alertify.service';
import { LookUpService } from 'app/core/services/LookUp.service';
import { LookUp } from 'app/core/models/LookUp';
import { AuthService } from 'app/core/components/admin/login/Services/Auth.service';
import { environment } from 'environments/environment';
import { LogDto } from './models/LogDto';
import { LogDtoService } from './services/LogDto.service';


declare var jQuery: any;

@Component({
	selector: 'app-logDto',
	templateUrl: './logDto.component.html',
	styleUrls: ['./logDto.component.scss']
})
export class LogDtoComponent implements OnInit {

	

	logDtoList:LogDto[];
	logDto:LogDto=new LogDto();

	logDtoAddForm: FormGroup;


	logDtoId:number;

	constructor(private logDtoService:LogDtoService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

	ngOnInit() {

		this.getLogDtoList();		
	}

	getLogDtoList() {
		this.logDtoService.getLogDtoList().subscribe(data => {
			this.logDtoList = data
		});
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
