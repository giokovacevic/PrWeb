import type IQuiz from "../types/models/quiz/IQuiz"
import { API_URL } from "../utils/Config";

export const getAllQuizes = async ():Promise<IQuiz[]> => {
    try{
        const response = await fetch(`${API_URL}/quizes/all`);
        if(!response.ok) {
            throw new Error(`Error fetching in QuizService: getAllQuizes | ` + response.statusText);
        }
        const data:IQuiz[] = await response.json();
        console.log(data);
        return data;
    }catch(error) {
        console.log(`Error: QuizService: getAllQuizes | ` + error);
        throw error;
    }
}

export const getQuizById = async (id: number):Promise<IQuiz | null> => {
    try{
        const response = await fetch(`${API_URL}/quizes/${id}`);
        if(!response.ok) {
            throw new Error(`Error fetching in QuizService: getQuizById (${id}) | ` + response.statusText);
        }
        const data:IQuiz | null = await response.json();
        console.log(data);
        return data;
    }catch(error) {
        console.log(`Error: QuizService: getQuizById (${id}) | ` + error);
        throw error;
    }
}