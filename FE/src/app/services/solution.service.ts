import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ResultMessage } from "../models/result.model";

@Injectable({
    providedIn: 'root'
})
export class SolutionService {
    private apiUrl: string;

    constructor(
        private httpClient: HttpClient
    ) {
        // TODO: move it to a settings file
        this.apiUrl = 'https://localhost:5006';
    }

    propose(model: { nums: number[], target: number }) {
        return this.httpClient.post<ResultMessage>(`${this.apiUrl}/solution/solution`, model);
    }

    search(id: string) {
        const params = new HttpParams().set('solutionId', id);
        return this.httpClient.get<ResultMessage>(`${this.apiUrl}/solution/solution`, { params });
    }
}