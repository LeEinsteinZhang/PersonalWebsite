from flask import Flask, render_template, request, redirect, url_for, session, send_from_directory
import json
import os

app = Flask(__name__)
app.secret_key = '233'
app.url_map.strict_slashes = False

def load_content(page, lang):
    with open(os.path.join('content', page, f'{lang}.json'), 'r', encoding='utf-8') as file:
        content = json.load(file)
    # Load footer content
    with open(os.path.join('content', 'base', f'{lang}.json'), 'r', encoding='utf-8') as file:
        footer_content = json.load(file)
    content.update(footer_content)
    return content

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
    content = load_content('index', lang)
    return render_template('index.html', content=content, lang=lang, current_page='index')

@app.route('/resume/<filename>')
def download_resume(filename):
    return send_from_directory(os.path.join(app.root_path, 'static/resume'), filename)


@app.route('/<lang>/<experience>')
def experiences(lang, experience):
    if 'lang' not in session or session['lang'] != lang:
        session['lang'] = lang
    content = load_content(f'experiences/{experience}', lang)
    return render_template('experience.html', content=content, lang=lang, current_page=experience)

if __name__ == '__main__':
    app.run()
