from flask import Flask, render_template, request, redirect, url_for, session, send_from_directory
import json
import os

app = Flask(__name__)
app.secret_key = '233'
app.url_map.strict_slashes = False

def load_translation(page, lang):
    with open(os.path.join('translations', page, f'{lang}.json'), 'r', encoding='utf-8') as file:
        translations = json.load(file)
    # Load footer translations
    with open(os.path.join('translations', 'base', f'{lang}.json'), 'r', encoding='utf-8') as file:
        footer_translations = json.load(file)
    translations.update(footer_translations)
    return translations

@app.route('/')
def home():
    if 'lang' in session:
        lang = session['lang']
    else:
        lang = request.accept_languages.best_match(['zh', 'en'])
        if lang is None or lang not in ['zh', 'en']:
            lang = 'en'
        session['lang'] = lang
    return redirect(url_for('home_lang', lang=lang))

@app.route('/<lang>')
def home_lang(lang):
    if 'lang' not in session or session['lang'] != lang:
        session['lang'] = lang
    translations = load_translation('index', lang)
    return render_template('index.html', translations=translations, lang=lang, current_page='index')

@app.route('/resume/<filename>')
def download_resume(filename):
    return send_from_directory(os.path.join(app.root_path, 'static/resume'), filename)


@app.route('/<lang>/<experience>')
def experiences(lang, experience):
    if 'lang' not in session or session['lang'] != lang:
        session['lang'] = lang
    translations = load_translation(f'experiences/{experience}', lang)
    print(translations)
    return render_template('experience.html', translations=translations, lang=lang, current_page=experience)

if __name__ == '__main__':
    app.run()
