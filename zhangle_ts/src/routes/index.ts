import { Router } from 'express';
import { home, experience, switchLang } from '../controllers/contentController';
import path from 'path';

const router = Router();

router.get('/', home);
router.get('/switch_lang/:lang', switchLang);
router.get('/resume/:filename', (req, res) => {
  const filename = req.params.filename;
  res.sendFile(path.join(__dirname, '../../public/static/resume', filename));
});
router.get('/favicon.ico', (req, res) => {
  res.sendFile(path.join(__dirname, '../../public/static/icons/favicon.ico'));
});
router.get('/sitemap.xml', (req, res) => {
  res.sendFile(path.join(__dirname, '../../public/static/sitemap.xml'));
});
router.get('/:experience', experience);
router.use((req, res) => {
  const sess: any = req.session;
  const lang = sess.data.lang || 'en';
  const prev_url = sess.data.prev_url || '/';
  res.status(404).render('404', { lang, prev_url });
});

export default router;
