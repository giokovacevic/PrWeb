import { useRef, useState } from 'react';
import styles from './Auth.module.css';
import { useNavigate } from 'react-router-dom';
import type ILoginResponse from '../../types/responses/ILoginResponse';
import { authenticate, login } from '../../services/AuthService';
import type ILoginRequest from '../../types/requests/ILoginRequest';

const LoginLayout = () => {
    const usernameOrEmailRef = useRef<HTMLInputElement | null>(null);
    const passwordRef = useRef<HTMLInputElement | null>(null);
    const [feedback, setFeedback] = useState<string>("");
    const navigate = useNavigate();

    const handleSubmitClicked = async (e:React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        
        let loginRequest: ILoginRequest = {
            usernameOrEmail: usernameOrEmailRef.current?.value || "",
            password: passwordRef.current?.value || ""
        };

        try {
            const data:ILoginResponse = await login(loginRequest);
            if(data.token && data.user && authenticate(data.token, data.user)) {
                setFeedback("Success!");
            }else{
                setFeedback("Invalid Credentials");
            }
        } catch (error) {
            setFeedback("Invalid Credentials.");
        }
    }

    return (
        <div className='root'>
            <div className='login-form-wrapper'>
                <form className='login-form' onSubmit={handleSubmitClicked}>
                    <div className='title'>Login</div>
                    <div>
                        <input type='text' placeholder='username or email' required ref={usernameOrEmailRef}></input>
                    </div>
                    <div>
                        <input type='password' placeholder='password' required ref={passwordRef}></input>
                    </div>
                    <div>
                        <input type='submit' value="Sign in" ></input>
                    </div>
                    <div className='feedback'>{feedback}</div>
                </form>
            </div>
        </div>
    );
}
export default LoginLayout;