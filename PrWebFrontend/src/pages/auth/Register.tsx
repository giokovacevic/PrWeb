import { useEffect, useRef, useState } from "react";
import { useNavigate } from 'react-router-dom';
import Loading from "../../components/loading/Loading";
import { useAuth } from "../../contexts/AuthContext";
import { register } from "../../services/AuthService";
import type IRegisterResponse from "../../types/responses/IRegisterResponse";
import styles from './Auth.module.css';

const Register = () => {
    const {loading, user} = useAuth();
    const usernameRef = useRef<HTMLInputElement | null>(null);
    const passwordRef = useRef<HTMLInputElement | null>(null);
    const emailRef = useRef<HTMLInputElement | null>(null);
    const imageRef = useRef<HTMLInputElement | null>(null);
    const [feedback, setFeedback] = useState<string[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        if(!loading && user) {
            navigate("/" + user.role.name, {replace: true});
        }
    }, [user, loading, navigate]);

     if(loading) return <Loading></Loading>

     const handleSubmitClicked = async (e:React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const username = usernameRef.current?.value || "";
        const password = passwordRef.current?.value || "";
        const image = imageRef.current?.files?.[0] || null;

        let formData:FormData = new FormData();
        formData.append("username", username);
        formData.append("password", password);
        if(image) formData.append("image", image);
  

        const data:IRegisterResponse = await register(formData);
        if(!data.successful) {
            setFeedback(data.messages);
        }else{
            navigate("/auth/login");
        }
     }
    
    return (
        <div className='root'>
            <div className='register-form-wrapper'>
                <form className='login-form' onSubmit={handleSubmitClicked}>
                    <div className='title'>Player Registration</div>
                    <div>
                        <input type='text' placeholder='Username' required ref={usernameRef}></input>
                    </div>
                    <div>
                        <input type='password' placeholder='Password' required ref={passwordRef}></input>
                    </div>
                    <div>
                        <input type='text' placeholder='Email' required ref={emailRef}></input>
                    </div>
                    <div>
                        <input type='file' accept="image/*" required ref={imageRef}></input>
                    </div>
                    <div>
                        <a href='/auth/login'>Back to login</a>
                        <input type='submit' value="Confirm" ></input>
                    </div>
                    <div className='feedback'>{feedback}</div>
                </form>
            </div>
        </div>
    );
}
export default Register;