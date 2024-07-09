import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ResultMessage } from "../models/result.model";
import { environment } from "../../environments/environment";

@Injectable({
    providedIn: 'root'
})
export class SolutionService {
    private apiUrl: string;

    constructor(
        private httpClient: HttpClient
    ) {
        this.apiUrl = environment.apiUrl;
    }

    propose(model: { nums: number[], target: number }) {
        return this.httpClient.post<ResultMessage>(`${this.apiUrl}/solution`, model);
    }

    search(id: string) {
        const params = new HttpParams().set('solutionId', id);
        return this.httpClient.get<ResultMessage>(`${this.apiUrl}/solution`, { params });
    }
}