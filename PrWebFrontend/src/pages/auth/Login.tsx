import { useEffect, useRef, useState } from 'react';
import styles from './Auth.module.css';
import { useNavigate } from 'react-router-dom';
import type ILoginResponse from '../../types/responses/ILoginResponse';
import { authenticate, login } from '../../services/AuthService';
import type ILoginRequest from '../../types/requests/ILoginRequest';
import { useAuth } from '../../contexts/AuthContext';
import Loading from '../../components/loading/Loading';

const Login = () => {
    const {user, loading, handleLogin} = useAuth();
    const usernameOrEmailRef = useRef<HTMLInputElement | null>(null);
    const passwordRef = useRef<HTMLInputElement | null>(null);
    const [feedback, setFeedback] = useState<string>("");
    const navigate = useNavigate();

    useEffect(() => {
        if(!loading && user) {
            navigate("/" + user.role.name, {replace: true});
        }
    }, [user, loading, navigate]);

     if(loading) return <Loading></Loading>

    const handleSubmitClicked = async (e:React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        
        let loginRequest: ILoginRequest = {
            usernameOrEmail: usernameOrEmailRef.current?.value || "",
            password: passwordRef.current?.value || ""
        };

        try {
            const data:ILoginResponse = await login(loginRequest);
            if(data.token && data.user && authenticate(data.token, data.user)) {
                handleLogin(data.token, data.user);
            }else{
                setFeedback("Invalid Credentials");
            }
        } catch (error) {
            setFeedback("Invalid Credentials.");
        }
    }

    return (
        <div className={styles.root}>
            <div className='login-form-wrapper'>
                <form className='login-form' onSubmit={handleSubmitClicked}>
                    <div className='title'>Login</div>
                    <div>
                        <input type='text' placeholder='Username or Email' required ref={usernameOrEmailRef}></input>
                    </div>
                    <div>
                        <input type='password' placeholder='Password' required ref={passwordRef}></input>
                    </div>
                    <div>
                        <a href='/auth/register'>Register as player</a>
                        <input type='submit' value="Sign in" ></input>
                    </div>
                    <div className='feedback'>{feedback}</div>
                </form>
            </div>
        </div>
    );
}
export default Login;