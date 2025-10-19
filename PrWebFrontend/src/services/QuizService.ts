import type IQuiz from "../types/models/quiz/IQuiz"
import type IQuizResult from "../types/models/quiz/IQuizResult";
import { API_URL } from "../utils/Config";

export const getAllQuizzes = async ():Promise<IQuiz[]> => {
    try{
        const response = await fetch(`${API_URL}/quizzes/all`);
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

export const getQuizById = async (id: string):Promise<IQuiz | null> => {
    try{
        const response = await fetch(`${API_URL}/quizzes/${id}`);
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

export const postQuizResults = async (quizResults:IQuizResult) => {
    try {
        console.log(quizResults);
        await fetch(`${API_URL}/quizzes/quiz-result`, {
            method: 'POST',
            headers: {'Content-Type':'application/json'},
            body: JSON.stringify(quizResults)
        });

    } catch (error) {
        console.log(`Error: QuizService: postQuizResults | ` + error);
    }
}