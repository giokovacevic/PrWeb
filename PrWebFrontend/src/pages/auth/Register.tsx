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
        const email = emailRef.current?.value || "";
        const image = imageRef.current?.files?.[0] || null;

        let formData:FormData = new FormData();
        formData.append("username", username);
        formData.append("password", password);
        formData.append("email", email);
        if(image) formData.append("image", image);
  

        const data:IRegisterResponse = await register(formData);
        if(!data.successful) {
            setFeedback(data.messages);
        }else{
            navigate("/auth/login");
        }
     }
    
    return (
        <div className={styles.root}>
            <div className={styles.register_form_wrapper}>
                <form className={styles.register_form} onSubmit={handleSubmitClicked}>
                    <div className={styles.title}>Player Registration</div>
                    <div className={styles.field}>
                        <input type='text' placeholder='Username' required ref={usernameRef}></input>
                    </div>
                    <div className={styles.field}>
                        <input type='password' placeholder='Password' required ref={passwordRef}></input>
                    </div>
                    <div className={styles.field}>
                        <input type='text' placeholder='Email' required ref={emailRef}></input>
                    </div>
                    <div className={styles.file}>
                        <input type='file' accept="image/*" required ref={imageRef}></input>
                    </div>
                    <div className={styles.button}>
                        <input type='submit' value="Confirm" ></input>
                    </div>
                    <div className={styles.feedback}>{feedback.map((value, index) => <span key={index}>{value}</span>)}</div>
                    <div className={styles.link}>
                        <a href='/auth/login'>&lt;&nbsp;Back to login</a>
                    </div>
                </form>
            </div>
        </div>
    );
}
export default Register;