import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'https://localhost:7039/api/v1'; // Update with your API base URL
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) {}

  // Master Table APIs
  // Business Activities
  getAllBusinessActivities(): Observable<any> {
    return this.http.get(`${this.baseUrl}/BusinessActivity/GetAll`);
  }

  addBusinessActivity(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/BusinessActivity/Add`, data, this.httpOptions);
  }

  updateBusinessActivity(data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/BusinessActivity/Update`, data, this.httpOptions);
  }

  deleteBusinessActivity(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/BusinessActivity/Delete?id=${id}`);
  }

  getBusinessActivityById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/BusinessActivity/GetById?id=${id}`);
  }

  // Insurance Classes
  getAllInsuranceClasses(): Observable<any> {
    return this.http.get(`${this.baseUrl}/InsuranceClass/GetAll`);
  }

  addInsuranceClass(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/InsuranceClass/Add`, data, this.httpOptions);
  }

  updateInsuranceClass(data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/InsuranceClass/Update`, data, this.httpOptions);
  }

  deleteInsuranceClass(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/InsuranceClass/Delete?id=${id}`);
  }

  // Insurance LOB
  getAllInsuranceLOBs(): Observable<any> {
    return this.http.get(`${this.baseUrl}/InsuranceLOB/GetAll`);
  }

  getLOBByClassId(classId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/InsuranceLOB/GetByClassId?classId=${classId}`);
  }

  addInsuranceLOB(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/InsuranceLOB/Add`, data, this.httpOptions);
  }

  updateInsuranceLOB(data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/InsuranceLOB/Update`, data, this.httpOptions);
  }

  deleteInsuranceLOB(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/InsuranceLOB/Delete?id=${id}`);
  }

  // Insurance Companies
  getAllInsuranceCompanies(): Observable<any> {
    return this.http.get(`${this.baseUrl}/InsuranceComp/GetAll`);
  }

  addInsuranceComp(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/InsuranceComp/Add`, data, this.httpOptions);
  }

  getInsuranceCompanyByName(name: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/InsuranceComp/GetByName?name=${name}`);
  }

  updateInsuranceComp(data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/InsuranceComp/Update`, data, this.httpOptions);
  }

  deleteInsuranceComp(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/InsuranceComp/Delete?id=${id}`);
  }

  // Insurance Products
  getAllInsuranceProducts(): Observable<any> {
    return this.http.get(`${this.baseUrl}/InsuranceProduct/GetAll`);
  }

  getInsuranceProductByInsuranceId(insuranceId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/InsuranceCompProduct/GetByInsuranceId?insuranceId=${insuranceId}`);
  }

  addInsuranceProduct(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/InsuranceProduct/Add`, data, this.httpOptions);
  }

  updateInsuranceProduct(data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/InsuranceProduct/Update`, data, this.httpOptions);
  }

  deleteInsuranceProduct(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/InsuranceProduct/Delete?id=${id}`);
  }

  getInsuranceProductById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/InsuranceProduct/GetById?id=${id}`);
  }

  // Insurance Contacts
  getAllInsuranceContacts(): Observable<any> {
    return this.http.get(`${this.baseUrl}/InsuranceContact/GetAll`);
  }

  getInsuranceContactByInsuranceId(insuranceId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/InsuranceCompContact/GetByInsuranceId?insuranceId=${insuranceId}`);
  }

  addInsuranceContact(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/InsuranceContact/Add`, data, this.httpOptions);
  }

  updateInsuranceContact(data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/InsuranceContact/Update`, data, this.httpOptions);
  }

  deleteInsuranceContact(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/InsuranceContact/Delete?id=${id}`);
  }

  getInsuranceContactById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/InsuranceContact/GetById?id=${id}`);
  }

  // Locations
  getAllLocations(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Location/GetAll`);
  }

  addLocation(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Location/Add`, data, this.httpOptions);
  }

  updateLocation(data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/Location/Update`, data, this.httpOptions);
  }

  deleteLocation(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Location/Delete?id=${id}`);
  }

  getLocationById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/Location/GetById?id=${id}`);
  }

  // Nationalities
  getAllNationalities(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Nationality/GetAll`);
  }

  addNationality(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Nationality/Add`, data, this.httpOptions);
  }

  updateNationality(data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/Nationality/Update`, data, this.httpOptions);
  }

  deleteNationality(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Nationality/Delete?id=${id}`);
  }

  getNationalityById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/Nationality/GetById?id=${id}`);
  }

  // Policy Types
  getAllPolicyTypes(): Observable<any> {
    return this.http.get(`${this.baseUrl}/PolicyType/GetAll`);
  }

  addPolicyType(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/PolicyType/Add`, data, this.httpOptions);
  }

  updatePolicyType(data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/PolicyType/Update`, data, this.httpOptions);
  }

  deletePolicyType(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/PolicyType/Delete?id=${id}`);
  }

  getPolicyTypeById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/PolicyType/GetById?id=${id}`);
  }

  // Source of Income
  getAllSourceOfIncomes(): Observable<any> {
    return this.http.get(`${this.baseUrl}/SourceOfIncome/GetAll`);
  }

  addSourceOfIncome(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/SourceOfIncome/Add`, data, this.httpOptions);
  }

  updateSourceOfIncome(data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/SourceOfIncome/Update`, data, this.httpOptions);
  }

  deleteSourceOfIncome(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/SourceOfIncome/Delete?id=${id}`);
  }

  getSourceOfIncomeById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/SourceOfIncome/GetById?id=${id}`);
  }

  // Financial APIs
  // Accounts
  getAllAccounts(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Account/GetAll`);
  }

  addAccount(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Account/Add`, data, this.httpOptions);
  }

  updateAccount(data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/Account/Update`, data, this.httpOptions);
  }

  deleteAccount(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Account/Delete?id=${id}`);
  }

  // Insurance Company Financial Operations
  approveInsuranceComp(id: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/InsuranceComp/Approve?id=${id}`, {}, this.httpOptions);
  }

  rejectInsuranceComp(id: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/InsuranceComp/Reject?id=${id}`, {}, this.httpOptions);
  }

  generateAccounts(insuranceId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/InsuranceComp/GenerateAccounts?insuranceId=${insuranceId}`, {}, this.httpOptions);
  }

  // Client APIs
  getAllClients(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Client/getAll`);
  }

  getClientById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/Client/getById?id=${id}`);
  }

  addClient(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Client/add`, data, this.httpOptions);
  }

  updateClient(data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/Client/update`, data, this.httpOptions);
  }

  deleteClient(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Client/delete?id=${id}`);
  }

  approveClient(id: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/Client/approve?id=${id}`, {}, this.httpOptions);
  }

  rejectClient(id: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/Client/reject?id=${id}`, {}, this.httpOptions);
  }

  blockClient(id: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/Client/block?id=${id}`, {}, this.httpOptions);
  }
}
