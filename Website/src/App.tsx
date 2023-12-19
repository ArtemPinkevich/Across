import { Routes, Route, BrowserRouter } from "react-router-dom";
import Main from "./Main/Main";
import Privacy from "./PrivacyPolicy/Privacy";
import Terms from "./TermsAndConditions/Terms";

export default function App() {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Main />} />
          <Route path="privacy" element={<Privacy />} />
          <Route path="terms" element={<Terms />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}
