export class LogDto {
	id?: number;
	tenantId?:string;
	level?: string;
	exceptionMessage?: string;
	timeStamp?: Date
	user?: string;
	value?: string;
	type?: string;
}